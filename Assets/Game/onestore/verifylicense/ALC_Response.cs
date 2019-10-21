using System;
using System.Text;

namespace OneStore
{
    [Serializable]
    public class ALC_Response
    {
        public string license;
        public string signature;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("lincense: " + license);
            sb.Append("signature: " + signature + "\n");
            return sb.ToString();
        }
    }

    [Serializable]
    public class ErrorResult
    {
        public int code;
        public string description;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("code: " + code + "\n");
            if (description != null && description.Length > 0)
            {
                sb.Append("description: " + description + "\n");
            }
            return sb.ToString();
        }
    }
}
