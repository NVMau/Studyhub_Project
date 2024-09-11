using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Common.Req
{
    namespace StudyHub.Common.Req
    {
        public abstract class BaseReq<T>
        {
            #region -- Methods --

            /// <summary>
            /// Initialize
            /// </summary>
            public BaseReq()
            {
                Keyword = null; // Explicitly allow null
            }

            /// <summary>
            /// Initialize with an ID
            /// </summary>
            /// <param name="id">ID</param>
            public BaseReq(int id)
            {
                Id = id;
                Keyword = null; // Explicitly allow null
            }

            /// <summary>
            /// Initialize with a keyword
            /// </summary>
            /// <param name="keyword">Keyword</param>
            public BaseReq(string keyword)
            {
                Keyword = keyword;
            }

            /// <summary>
            /// Convert the request to the model
            /// </summary>
            /// <param name="createdBy">Created by</param>
            /// <returns>Return the result</returns>
            public abstract T ToModel(int? createdBy = null);

            #endregion

            #region -- Properties --

            /// <summary>
            /// ID
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Keyword, can be null
            /// </summary>
            public string? Keyword { get; set; }

            #endregion
        }
    }
}