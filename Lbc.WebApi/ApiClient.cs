using System;
using System.Threading.Tasks;

namespace Lbc.WebApi {
    public class ApiClient {
        public static readonly string BaseUrl = "http://api.lbc.tunnel.mobi/api/v1/framework";

        public static event EventHandler<ApiExecutedEventArgs> OnMethodExecuted;

        private static string Token {
            get;
            set;
        }

        public static void SetToken(string token) {
            Token = token;
        }

        public static string GetMethodUrl(string methodName) {
            return string.Format("{0}/{1}", BaseUrl, methodName);
        }

        public async static Task<T> Execute<T>(MethodBase<T> method) {
            try {
                return await method.Execute(Token)
                    .ContinueWith((t) => {
                        if (OnMethodExecuted != null) {
                            OnMethodExecuted.Invoke(null, new ApiExecutedEventArgs() {
                                ErrorResion = method.ErrorReason,
                                StatusCode = method.Status,
                                HasError = method.HasError
                            });
                        }
                        return t.Result;
                    });
                //无用，如果是一个错误的地址，Android 下会抛出 WebException ，但是并不会进这里来。
                //WP下也进不来
                //.ContinueWith(t => {
                //    return t.Result;
                //}, TaskContinuationOptions.OnlyOnFaulted)
            } catch (Exception ex) {
                if (OnMethodExecuted != null) {
                    OnMethodExecuted.Invoke(null, new ApiExecutedEventArgs() {
                        ErrorResion = ex.GetBaseException().Message,
                        StatusCode = method.Status,
                        HasError = true
                    });
                }
                return default(T);
            }
        }
    }
}
