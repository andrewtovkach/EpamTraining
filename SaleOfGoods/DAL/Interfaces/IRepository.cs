﻿using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(T item);
        void Update(int id, T item);
        IEnumerable<T> Items { get; }
    }
}
