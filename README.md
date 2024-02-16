# nauta-portal
Script to access to the Nauta Portal

## For help on how to use pyenv-win and python-poetry, please refer to the online documentation:

pyenv-win - https://github.com/pyenv-win/pyenv-win#usage
python-poetry - https://python-poetry.org/


## Install dependencies

run the following from the same directory:
```bash
poetry shell
```

## build binary using pyinstaller
```bash
poetry run pyinstaller --onefile -n portal_nauta main.py
```

with upx, run:

```bash
poetry run pyinstaller --onefile -n portal_nauta --upx-dir=./ main.py
```