// 
// System.Web.HttpException
//
// Authors:
//  Patrik Torstensson (Patrik.Torstensson@labs2.com)
//  Gonzalo Paniagua Javier (gonzalo@ximian.com)
//
// (c) 2002 Patrik Torstensson
// (c) 2003 Ximian, Inc. (http://www.ximian.com)
// Copyright (C) 2005-2009 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;
using System.Collections.Specialized;

namespace System.Web
{
    [Serializable]
    public class HttpException : ExternalException
    {
        public HttpException ()
        {
        }
        
        public HttpException (string message)
            : base (message)
        {
        }
        
        public HttpException (string message, Exception innerException)
            : base (message, innerException)
        {
        }
        
        public HttpException (int httpCode, string message) : base (message)
        {
            this.ErrorCode = httpCode;
        }
        
        public HttpException (int httpCode, string message, Exception innerException)
            : base (message, innerException)
        {
            this.ErrorCode = httpCode;
        }

        public new int ErrorCode { get; private set; }
    }
}
