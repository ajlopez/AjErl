-module(tail).

tail(0, Y) -> Y;
tail(X, Y) -> tail(X-1,Y+1).