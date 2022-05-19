# pokemon_challenge

Notes before using in production:
- Unit tests can probably be optimised with setup, sequences
- Enable caching
- Implement throttling

Create and run docker container:
1. cd pokemon_challenge
2. docker build -t pokemonchallengeapi .
3. docker run -d -p 8080:80 --name pokemonchallengeapp pokemonchallengeapi
4. go to http://localhost:8080/swagger/index.html
