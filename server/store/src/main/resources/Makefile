default:
	echo 'Make test!'

migrate create:
	migrate create -ext sql -dir ./migration -seq init

migrate up:
	migrate -path db/migration -database 'postgresql://admin:52670@localhost/store' up

migrate down:
	migrate -path db/migration -database 'postgresql://admin:52670@localhost/store' down -all