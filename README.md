# TOML Serialization

[![openupm](https://img.shields.io/npm/v/com.underlogic.toml-serialization?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.underlogic.toml-serialization/)

Unity package library for serializing and deserializing [TOML](https://toml.io/en/) data.

## Installation

The package library can be installed to your Unity project in the following ways:

- Using [OpenUPM](https://openupm.com/) package manager
  - `openupm add com.underlogic.toml-serialization`
- Clone the repository and adding it as a local package
- Add the package by git URL

Installing it by git URL allows the package to be updated when new releases are available here.

## Documentation

[Online documentation](https://underlogic.github.io/toml-serialization/) is automatically published to GitHub Pages.

Offline docs can be found in the [`Documentation~`](./Documentation~) folder, and can be viewed via [`mdbook`](https://rust-lang.github.io/mdBook/index.html):

```shell
$ mdbook serve ./Documentation~ --open
```

## Samples

The package library includes several sample scenes that demonstrate how to serialize and deserialize data.
It is highly recommended that you import the samples so you can see the library in action and experiment with it.

## Contributing

To make working on the library easier while keeping this repository minimal, it is recommended to create a separate Unity project, (i.e. `toml-serialization-project`).

Then link the following folders into the Unity project for easy editing/syncing while keeping code completion and intellisense:

```shell
$ cd toml-serialization-project/Assets
$ ln -s ~/toml-serialization/Samples~ Samples

$ mkdir -p Scripts && cd Scripts
$ ln -s ~/toml-serialization/Editor
$ ln -s ~/toml-serialization/Runtime
$ ln -s ~/toml-serialization/Tests
```

**NOTE:** This assumes both folders are in your `$HOME` folder. Adjust paths accordingly if they differ.

Now you can work in the shell project without copying files back and forth, and also generating `.meta` files automatically.
