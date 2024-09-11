using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyHub.Common.Rsp
{
    public class BaseRsp
    {
        #region -- Properties --

        public bool Success { get; private set; }
        public string? Code { get; set; }
        public string Message
        {
            get
            {
                if (Success)
                {
                    return msg;
                }
                else
                {
                    return Dev ? msg : err;
                }
            }
        }
        public string Variant
        {
            get { return Success ? "success" : "error"; }
        }
        public string Title
        {
            get { return Success ? "Success" : titleError; }
        }
        public static bool Dev { get; set; }

        #endregion

        #region -- Fields --

        private readonly string err;
        private readonly string titleError;
        private string msg;

        #endregion

        #region -- Constructors --

        public BaseRsp()
        {
            Success = true;
            msg = string.Empty;
            titleError = "Error";
            Dev = true; // TODO

            if (string.IsNullOrEmpty(err))
            {
                err = "Please update common error in Custom Settings";
            }
        }

        public BaseRsp(string message) : this()
        {
            msg = message;
        }

        public BaseRsp(string message, string titleError) : this(message)
        {
            this.titleError = titleError;
        }

        #endregion

        #region -- Methods --

        public void SetError(string message)
        {
            Success = false;
            msg = message;
        }

        public void SetError(string code, string message)
        {
            Success = false;
            Code = code;
            msg = message;
        }

        public void SetMessage(string message)
        {
            msg = message;
        }

        public void TestError()
        {
            SetError("We are testing to show error message, please ignore it...");
        }

        #endregion
    }
}
