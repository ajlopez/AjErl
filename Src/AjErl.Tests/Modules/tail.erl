-module(tail).
-export([tail/2]).

tail(0, Y) -> Y;
tail(X, Y) -> tail(X-1,Y+1).