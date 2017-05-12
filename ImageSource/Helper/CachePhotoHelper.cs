﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ImageSource.Helper
{
    public class CachePhotoHelper : PhotoHelper
    {
        private static volatile MemoryCache _localCache;
        private static object _lock = new object();

        public CachePhotoHelper(IRestClient restClient, ISerializer serializer)
            : base(restClient, serializer)
        {
            _localCache = MemoryCache.Default;
        }

        public new IQueryable<Photo> GetAll()
        {
            /////////////////////////////////////////////////////////////////////////////////////////////
            //TODO: Make your code changes here so we can keep the same reference value object all the time.
            //base.GetAll() should be called once irrespectively of how many instances of CachePhotoHelper 
            //are created. Implement MemoryCache to keep the results from base.GetAll() cached for 20 seconds.

            IQueryable<Photo> cache = (IQueryable<Photo>)_localCache.Get("AllPhotos");
            if (cache == null) {
                cache = base.GetAll();
                _localCache.Set("AllPhotos", base.GetAll(), new DateTimeOffset(DateTime.Now.AddSeconds(20)));
            }           

            return cache;

            /////////////////////////////////////////////////////////////////////////////////////////////
        }

        public new IQueryable<Photo> Match(Expression<Func<Photo, bool>> searchPattern)
        {
            var collection = this.GetAll();
            return collection.Where(searchPattern);
        }

        public new IQueryable<Photo> Match(Expression<Func<Photo, bool>> searchPattern, Expression<Func<Photo, object>> sorting)
        {
            return Match(searchPattern).OrderBy(sorting);
        }
    }
}
