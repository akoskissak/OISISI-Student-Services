﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GUI.View
{
    // Ova klasa je kopirana sa vebsajta: https://stackoverflow.com/questions/784726/how-can-i-paginate-a-wpf-datagrid
    // i prilagodjena nasoj aplikaciji.
    public class PagingCollectionView : CollectionView
    {
        private IList _innerList;
        private int _itemsPerPage;

        private int _currentPage = 1;

        public PagingCollectionView(IList innerList, int itemsPerPage)
            : base(innerList)
        {
            this._innerList = innerList;
            this._itemsPerPage = itemsPerPage;
        }

        public IList InnerList
        {
            get { return _innerList; }
            set { _innerList = value; }
        }

        public override int Count
        {
            get
            {
                if (this._innerList.Count == 0) return 0;
                if (this._currentPage < this.PageCount)
                {
                    return this._itemsPerPage;
                }
                else
                {
                    int itemsLeft = this._innerList.Count % this._itemsPerPage;
                    if (0 == itemsLeft)
                    {
                        return this._itemsPerPage;
                    }
                    else
                    {
                        return itemsLeft;
                    }
                }
            }
        }

        public int CurrentPage
        {
            get { return this._currentPage; }
            set
            {
                this._currentPage = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs("CurrentPage"));
            }
        }

        public int ItemsPerPage { get { return this._itemsPerPage; } }

        public int PageCount
        {
            get
            {
                return (this._innerList.Count + this._itemsPerPage - 1)
                    / this._itemsPerPage;
            }
        }

        private int EndIndex
        {
            get
            {
                int end = this._currentPage * this._itemsPerPage - 1;
                return (end > this._innerList.Count) ? this._innerList.Count : end;
            }
        }

        private int StartIndex
        {
            get { return (this._currentPage - 1) * this._itemsPerPage; }
        }

        public override object GetItemAt(int index)
        {
            int offset = index % (this._itemsPerPage);
            int itemIndex = this.StartIndex + offset;
            if (itemIndex >= 0 && itemIndex < this._innerList.Count)
            {
                return this._innerList[itemIndex];
            }
            else
            {
                return null;
            }
        }

        public void MoveToNextPage()
        {
            if (this._currentPage < this.PageCount)
            {
                this.CurrentPage += 1;
            }
            this.Refresh();
        }

        public void MoveToPreviousPage()
        {
            if (this._currentPage > 1)
            {
                this.CurrentPage -= 1;
            }
            this.Refresh();
        }

        public void MoveToFirstPage()
        {
            if (this._currentPage != 1)
            {
                this.CurrentPage = 1;
            }
            this.Refresh();
        }
    }
}
