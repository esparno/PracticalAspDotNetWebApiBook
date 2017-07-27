using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HelloWebApi
{
    public class EncodedContent : HttpContent
    {
        private HttpContent content;
        private Func<Stream, Stream> encoder;

        public EncodedContent(HttpContent content, Func<Stream,Stream> encoder)
        {
            if(content != null)
            {
                this.content = content;
                this.encoder = encoder;

                content.Headers.ToList().ForEach(x =>
                                this.Headers.TryAddWithoutValidation(x.Key, x.Value));
            }
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }

        protected async override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            using (content)
            {
                using (Stream encodedStream = encoder(stream))
                {
                    await content.CopyToAsync(encodedStream);
                }
            }
        }
    }
}