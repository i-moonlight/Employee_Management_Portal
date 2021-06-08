default:
	echo 'Make test!'

migrate create:
	migrate create -ext sql -dir ./migration -seq init

migrate up:
	migrate -path ./migration -database 'postgresql://admin:52670@localhost/auth' up

migrate down:
	migrate -path ./migration -database 'postgresql://admin:52670@localhost/auth' down -all