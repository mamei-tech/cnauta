# Nauta portal

## Description
Script to access to the Nauta Portal.

This project is implemented with Python 3 using the Poetry dependency management tool. To help on how to use python-poetry please refer to the [online documentation](https://python-poetry.org/):

## Installation

To install and set up the project, follow these steps:

### Clone the repository:
```bash
git clone https://github.com/mamei-tech/nauta-portal.git
```

### Navigate to the project directory:

```bash
cd nauta-portal/multiplatform/python
```

### Install Poetry:

```bash
curl -sSL https://install.python-poetry.org | python3 -
```

### Install project dependencies using Poetry:

```bash
poetry install
```

Note> If you use a virtual environment, we recommend following the steps in this [section](https://python-poetry.org/docs/basic-usage/#using-your-virtual-environment).


## Configuration

To configure the project, follow these steps:

Open the config.cfg file and add the `credentials` to authenticate in the Nauta portal:

```editorconfig
# it should be like this
[CREDENTIAL]
username = username@nauta.com
password = ThisIsMyPassword
```

## Usage

To run the project, follow these steps:

### Activate the project's virtual environment:

```bash
poetry shell
```

### Run the project:
```bash
python main.py
```
or:
```bash
poetry run python3 main.py
```

## Build binary using pyinstaller
```bash
poetry run pyinstaller --onefile -n portal_nauta main.py
```

with upx, run:

> Note: You must first download the upx. For the following example we place the upx binary in the root directory of the project
>
```bash
poetry run pyinstaller --onefile -n portal_nauta --upx-dir=./ main.py
```

## Contributing

If you would like to contribute to this project, please follow these guidelines:

1. Fork the repository.
2. Create a new branch:
    ```bash
      git checkout -b feature/your-feature
    ```
3. Make your changes and commit them:
    ```bash
      git commit -m "Add your commit message"
    ```
4. Push your changes to your forked repository:
    ```bash
      git push origin feature/your-feature
    ```
5. Open a pull request to the original repository.


## License

This project is licensed under the [GNU GENERAL PUBLIC LICENSE](./LICENSE).