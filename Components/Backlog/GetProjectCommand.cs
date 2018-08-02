using System;
using System.Threading.Tasks;
using Steeltoe.CircuitBreaker.Hystrix;

namespace Backlog
{
    public class GetProjectCommand<T>: HystrixCommand<T>
    {
        private readonly Func<long, Task<T>> _fn;
        private readonly long _Id;
        private readonly Func<long, Task<T>> _fallbackFn;

        public GetProjectCommand(
            Func<long, Task<T>> fn,
            Func<long, Task<T>> fallbackFn,
            long id
        ) : base(HystrixCommandGroupKeyDefault.AsKey(typeof(T).ToString()))
        {
            _fn = fn;
            _Id = id;
            _fallbackFn = fallbackFn;
        }

        protected override async Task<T> RunAsync() => await _fn(_Id);
        protected override async Task<T> RunFallbackAsync() => await _fallbackFn(_Id);
    }
}