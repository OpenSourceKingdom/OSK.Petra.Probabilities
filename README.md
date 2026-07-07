# OSK.Petra.Probabilities

This library provides a mechanism for adding and utilizing random probabilities in an application. This can be for normal random usage or for use with a probability roller to simulate dice rolls,
chance checks, and similar random requirements for an application or game.

## Usage

The central point is the `IProbabilityRoller` to handle probability and random checks. To setup and configure a roller with the dependency container, use `AddProbabilities`. Additionally,
a static factory class is available through `Probability` to create rollers independent of any DI containers or where such use cases are not viable for an application.

## RNG support

Random generators can be created and used for a roller by implementing the `IRandomNumberGenerator` interface and injecting it into the dependency injection before calling AddProbabilities or by utilizing
the `IProbabilityBuilder` to inject the custom generator that way. Additionally, custom generators can be injected via the static factory for probability rollers as well. The library currently supports the
following random number generators:
- `Seeded`: a basic RNG that utilizes a seed to generate random numbers. The caveat is that it can not easily be restored other than simply reseeding the generator. In some cases, this is undesired since the sequence of generation can become lost in a game replay -i.e. a turn that is saved and reloaded can then have a different outcome than if it was simply played through
- `PCG32`: an RNG that utilizes a state value that can be restored and utilized for random generation. This allows continuing a sequence directly from where it was left off