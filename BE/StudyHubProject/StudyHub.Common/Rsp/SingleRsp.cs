using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Common.Rsp
{
    public class SingleRsp : BaseRsp
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public SingleRsp() : base()
        {
            Data = null; // Khởi tạo Data là null để cho phép nullable
        }

        /// <summary>
        /// Initialize with a message
        /// </summary>
        /// <param name="message">Message</param>
        public SingleRsp(string message) : base(message)
        {
            Data = null; // Khởi tạo Data là null để cho phép nullable
        }

        /// <summary>
        /// Initialize with a message and a title for the error
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="titleError">Title error</param>
        public SingleRsp(string message, string titleError) : base(message, titleError)
        {
            Data = null; // Khởi tạo Data là null để cho phép nullable
        }

        /// <summary>
        /// Set data and update the response code
        /// </summary>
        /// <param name="code">Success code</param>
        /// <param name="data">Data</param>
        public void SetData(string code, object data)
        {
            Code = code;
            Data = data;
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Data, can be null
        /// </summary>
        public object? Data { get; set; }

        #endregion
    }
}
