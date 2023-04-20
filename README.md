# HasteUpPooling

Modular Pools & Factory for unity

# Get Started

## Installation

Add UPM package via URL `https://github.com/FrancoisSestier/HasteUpPooling.git` 
https://docs.unity3d.com/2019.3/Documentation/Manual/upm-ui-giturl.html

## Usage

Add the Factory component to a GO

### PoolData

This is a extentable Scriptable object containing meta data of one pool. 
mainely the prefab to be instanciated.
The prefab must contain one component at it's root that implements the interface IPoolable.

### Pools 

You can create custom pool scripts by extending AbstractPool.
pool type is to be specified in the PoolData.

### Factory 

Spawn object through the factory API to avoid Instantiation 

