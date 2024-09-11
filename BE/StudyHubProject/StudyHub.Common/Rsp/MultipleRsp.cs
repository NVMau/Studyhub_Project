using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Common.Rsp
{
    public class MultipleRsp : BaseRsp
    {
        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public MultipleRsp() : base()
        {
            Data = new Dictionary<string, object>();  // Khởi tạo rõ ràng
        }

        /// <summary>
        /// Initialize with a message
        /// </summary>
        /// <param name="message">Message</param>
        public MultipleRsp(string message) : base(message)
        {
            Data = new Dictionary<string, object>();  // Khởi tạo rõ ràng
        }

        /// <summary>
        /// Initialize with a message and a title for the error
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="titleError">Title error</param>
        public MultipleRsp(string message, string titleError) : base(message, titleError)
        {
            Data = new Dictionary<string, object>();  // Khởi tạo rõ ràng
        }

        /// <summary>
        /// Set data
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="o">Data</param>
        public void SetData(string key, object o)
        {
            Data[key] = o;
        }

        /// <summary>
        /// Set success data
        /// </summary>
        /// <param name="o">Data</param>
        /// <param name="message">Message</param>
        public void SetSuccess(object o, string message)
        {
            var t = new Dto(o, message);
            SetData("success", t);
        }

        /// <summary>
        /// Set failure data
        /// </summary>
        /// <param name="o">Data</param>
        /// <param name="message">Message</param>
        public void SetFailure(object o, string message)
        {
            var t = new Dto(o, message);
            SetData("failure", t);
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// Data
        /// </summary>
        public Dictionary<string, object> Data { get; private set; }

        #endregion

        #region -- Classes --

        /// <summary>
        /// Data transfer object
        /// </summary>
        public class Dto
        {
            #region -- Methods --

            /// <summary>
            /// Initialize
            /// </summary>
            /// <param name="data">Data</param>
            /// <param name="message">Message</param>
            public Dto(object data, string message)
            {
                Data = data;
                Message = message;
            }

            #endregion

            #region -- Properties --

            /// <summary>
            /// Data
            /// </summary>
            public object Data { get; private set; }

            /// <summary>
            /// Message
            /// </summary>
            public string Message { get; private set; }

            #endregion
        }

        #endregion
    }
}
