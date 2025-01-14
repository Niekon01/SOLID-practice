﻿using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Database
{
    public class EntryDatabase : IDatabase
    {
        private List<IEntry> _entryList = new List<IEntry>();

        public bool AddEntry(IEntry entry)
        {
            _entryList.Add(entry);
            _entryList.Sort(
                    (a, b) => a.DateCreated < b.DateCreated ? 1 : -1);
            return true;
        }

        public List<IEntry> ReturnOneType(EntryType etype)
        {
            List<IEntry> oneType = _entryList.FindAll(e => e.EType == etype);

            return oneType;
        }

        public List<IEntry> ReturnAll()
        {
            return _entryList;
        }
    }
}
