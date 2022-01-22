# Getting Started with Vanilla JS App

This project was bootstrapped with [Create App](http://vanilla-js.com/).

## Available Scripts

In the project directory, you can run:

#### `npm init`

#### `npm install -d live-server`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in your browser.

The page will reload when you make changes.\
You may also see any lint errors in the console.

#### `npm install -D webpack webpack-cli webpack-dev-server`
Install webpack

```json
package.json

"scripts": {
"start": "live-server --port=3000 src --verbose",
"start": "webpack-dev-server --port=3000 --open",
"test": "echo \"Error: no test specified\" && exit 1"
}
```

### install jslint by iDEA plugin
Установка линтера через плагин другие установки и настройки избыточны.

Очистка кэша
#### `npm cache clean --force`
#### `npm cache verify`

Удаление неиспользуемых модулей в папке node_modules
#### `npm install rimraf -g`
#### `npx rimraf node_modules`
#### `npm ci`


Очистка неиспользуемых зависимостей
#### `npm prune`

Вывод список используемых зависимостей
#### `npm ls`
