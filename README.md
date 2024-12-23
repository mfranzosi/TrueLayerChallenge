# How to run

- Download and install the latest version of Docker (www.docker.com), if not already installed.
- Run the following command to pull the PokedexApi image:

```
Docker pull massimofranzosi/pokedexapi
```

- Run the image with the following command:

```
docker run -p 8080 -p 8081 -d massimofranzosi/pokedexapi
```

- Check if the container is running and the dynamically assigned ports:

```
docker ps
```

- Open a browser the following sample queries, being careful of specifying the assigned port:

```
http://localhost:{your_http_port}/pokemon/charizard´
http://localhost:{your_http_port}/pokemon/translated/charizard´
```

# Anything I’d do differently for a production API

- Add caching, and return locally cached data if available. This would spare 2 external calls, implying both performance boost and money saving.
- Add OpenTelemetry, in order to collect telemetry data such as metrics, logs, and traces. This would help to analyze software performance and behavior.
- I would add some extra validation rules for the Pokemon name in the exposed endpoints. For example, if the name length is greater than 50, or if it contains non-alphanumeric characters, I would already return an error.
- It would be nice to have some API rate limiting
- I would also add authentication/authorization if the API is not public.
- I would make the language configurable, so that the description is returned based on the user's settings (considering that Yoda/Shakespeare translators would not work!)
 





# 