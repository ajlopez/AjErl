-module(fibo).
-export([fibo/1]).

fibo(0) -> 1;
fibo(1) -> 1;
fibo(X) -> fibo(X-1) + fibo(X-2).
