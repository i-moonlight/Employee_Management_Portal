<p align="center">
  <a href="http://nestjs.com/" target="blank"><img src="https://nestjs.com/img/logo-small.svg" width="200" alt="Nest Logo" /></a>
</p>

[circleci-image]: https://img.shields.io/circleci/build/github/nestjs/nest/master?token=abc123def456
[circleci-url]: https://circleci.com/gh/nestjs/nest

  <p align="center">A progressive <a href="http://nodejs.org" target="_blank">Node.js</a> framework for building efficient and scalable server-side applications.</p>
    <p align="center">
<a href="https://www.npmjs.com/~nestjscore" target="_blank"><img src="https://img.shields.io/npm/v/@nestjs/core.svg" alt="NPM Version" /></a>
<a href="https://www.npmjs.com/~nestjscore" target="_blank"><img src="https://img.shields.io/npm/l/@nestjs/core.svg" alt="Package License" /></a>
<a href="https://www.npmjs.com/~nestjscore" target="_blank"><img src="https://img.shields.io/npm/dm/@nestjs/common.svg" alt="NPM Downloads" /></a>
<a href="https://circleci.com/gh/nestjs/nest" target="_blank"><img src="https://img.shields.io/circleci/build/github/nestjs/nest/master" alt="CircleCI" /></a>
<a href="https://coveralls.io/github/nestjs/nest?branch=master" target="_blank"><img src="https://coveralls.io/repos/github/nestjs/nest/badge.svg?branch=master#9" alt="Coverage" /></a>
<a href="https://discord.gg/G7Qnnhy" target="_blank"><img src="https://img.shields.io/badge/discord-online-brightgreen.svg" alt="Discord"/></a>
<a href="https://opencollective.com/nest#backer" target="_blank"><img src="https://opencollective.com/nest/backers/badge.svg" alt="Backers on Open Collective" /></a>
<a href="https://opencollective.com/nest#sponsor" target="_blank"><img src="https://opencollective.com/nest/sponsors/badge.svg" alt="Sponsors on Open Collective" /></a>
  <a href="https://paypal.me/kamilmysliwiec" target="_blank"><img src="https://img.shields.io/badge/Donate-PayPal-ff3f59.svg"/></a>
    <a href="https://opencollective.com/nest#sponsor"  target="_blank"><img src="https://img.shields.io/badge/Support%20us-Open%20Collective-41B883.svg" alt="Support us"></a>
  <a href="https://twitter.com/nestframework" target="_blank"><img src="https://img.shields.io/twitter/follow/nestframework.svg?style=social&label=Follow"></a>
</p>
  <!--[![Backers on Open Collective](https://opencollective.com/nest/backers/badge.svg)](https://opencollective.com/nest#backer)
  [![Sponsors on Open Collective](https://opencollective.com/nest/sponsors/badge.svg)](https://opencollective.com/nest#sponsor)-->

## Description

[Nest](https://github.com/nestjs/nest) framework TypeScript starter repository.

## Installation

```bash
$ npm install
```

## Running the app

```bash
# development
$ npm run start

# watch mode
$ npm run start:dev

# production mode
$ npm run start:prod
```

## Test

```bash
# unit tests
$ npm run test

# e2e tests
$ npm run test:e2e

# test coverage
$ npm run test:cov
```

## Support

Nest is an MIT-licensed open source project. It can grow thanks to the sponsors and support by the amazing backers. If you'd like to join them, please [read more here](https://docs.nestjs.com/support).

## Stay in touch

- Author - [Kamil Myśliwiec](https://kamilmysliwiec.com)
- Website - [https://nestjs.com](https://nestjs.com/)
- Twitter - [@nestframework](https://twitter.com/nestframework)

## License

Nest is [MIT licensed](LICENSE).


## Prisma
****

````
$ npm install prisma --save-dev

$ npx prisma init

$ npx prisma db push
````

В процессе разработки:
  - Никогда не используйте db push. За исключением прототипирования.
    - После первой миграции вы должны использовать prisma migrate dev вместо этого.
    
Как только вы выполните свою первую миграцию, используйте prisma migrate dev.
- Он предупредит вас о любой потере данных и спросит, какова цель миграции (например, git commit -m).
    - В конце будет создан новый файл миграции и применен к вашей базе данных для разработки.
      - Если вам нужно отредактировать миграцию, чтобы предотвратить потерю данных, ознакомьтесь с этой документацией.
- Вот процесс:
- Используйте prisma migrate dev --create-only.
  - Отредактируйте автоматически созданный файл переноса (вам нужно будет написать его SQL самостоятельно)
    - Затем примените отредактированную миграцию с помощью prisma migrate dev.
      - Благодаря этому процессу у вас будет migrations папка со всеми вашими миграциями, которая будет отслеживать эволюцию схемы вашей базы данных.
    - При необходимости вы сможете выполнить откат к любой предыдущей миграции и сможете развернуть свои миграции в рабочей базе данных.


- В вашем CI / CD вам придется запускать prisma migrate deploy вместо prisma migrate dev.
- Эта команда допускает потерю данных и не запрашивает у вас имя переноса (чтобы избежать блокировки вашего конвейера сборки).
- Эта команда применит все миграции, которые еще не применены.


## Nest generate commands
````
$ npx nest g resource [feature folder name]
$ npx nest g resource [feature folder name] --no-spec
````

## Npm libs commands
````
$ npm install class-validator --save
$ npm i argon2
$ npm install class-transformer
````
