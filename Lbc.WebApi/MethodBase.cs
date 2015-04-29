using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Lbc.WebApi {

    public abstract class MethodBase {

        public virtual bool SupportProtoBuf {
            get {
                return true;
            }
        }

        public bool HasError {
            get;
            protected set;
        }

        public string ErrorReason {
            get;
            protected set;
        }

        public HttpStatusCode? Status {
            get;
            protected set;
        }

        /// <summary>
        /// 方法名,除去基地址,比如 User/Get
        /// </summary>
        public abstract string MethodName {
            get;
        }

        /// <summary>
        /// 如何执行,是Post,get还是 delete 等, 执行的参数等.
        /// </summary>
        internal abstract Func<HttpClient, Uri, Task<HttpResponseMessage>> Invoke {
            get;
        }


        internal async virtual Task<HttpResponseMessage> GetResult(string token) {
            Uri url = new Uri(ApiClient.GetMethodUrl(this.MethodName));

            if (this.Invoke != null) {
                using (var client = new OAuthHttpClient(token)) {
                    if (this.SupportProtoBuf)
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-protobuf"));

                    return await this.Invoke.Invoke(client, url);
                }
            }

            return await Task.FromResult<HttpResponseMessage>(null);
        }
    }

    public abstract class MethodBase<T> : MethodBase {

        internal async virtual Task<T> Execute(string token) {
            var a = await this.GetResult(token);
            var reason = "";
            HttpStatusCode? status = null;
            if (a != null) {
                if (a.IsSuccessStatusCode) {
                    //var str = await a.Content.ReadAsStringAsync();
                    //var o = JsonConvert.DeserializeObject<T>(str);

                    if (this.SupportProtoBuf) {
                        return await a.Content.ReadAsAsync<T>(new[] { new ProtoBufFormatter() });
                    } else
                        return await a.Content.ReadAsAsync<T>();
                } else {
                    reason = a.ReasonPhrase;
                    status = a.StatusCode;
                }
            }

            this.HasError = true;
            this.ErrorReason = reason;
            this.Status = status;

            return await Task.FromResult<T>(default(T));
        }

    }
}
