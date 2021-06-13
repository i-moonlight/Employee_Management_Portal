default:
	migrate -version

migrate create:
	migrate create -ext sql -dir ./migration -seq init

migrate up:
	migrate -path ./migration -database 'postgresql://${POSTGRES_USERNAME}:${POSTGRES_PASSWORD}@localhost/${POSTGRES_DATABASE}' up

migrate down:
	migrate -path ./migration -database 'postgresql://${POSTGRES_USERNAME}:${POSTGRES_PASSWORD}@localhost/${POSTGRES_DATABASE}' down -all