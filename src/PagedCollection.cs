/* Copyright 2015 Naveego Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Naveego
{
    public class PagedCollection<T>
    {

        public PagedCollectionMetadata Meta { get; set; }

        public IEnumerable<T> Data { get; set; }

        public PagedCollection()
        {
            Meta = new PagedCollectionMetadata();
        }

        public PagedCollection(IEnumerable<T> data)
        {
            Data = data;
            Meta = new PagedCollectionMetadata
            {
                Page = 1,
                PageSize = 25,
                Count = data.Count()
            };
        }

        public PagedCollection(IEnumerable<T> data, int page, int pageSize, int count)
        {
            Data = data;
            Meta = new PagedCollectionMetadata
            {
                Page = page,
                PageSize = pageSize,
                Count = count
            };
        }

    }

}
