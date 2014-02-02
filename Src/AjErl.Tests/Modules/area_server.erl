-module(area_server).
-export([loop/0]).

loop() ->
	receive
		{Sender, rectangle, Width, Ht} ->
			Sender ! Width * Ht,
			loop();
		{Sender, circle, R} ->
			Sender ! 3.14159 * R * R,
			loop();
		stop ->
			stop;
		Other ->
			Sender ! error,
			loop()
end.