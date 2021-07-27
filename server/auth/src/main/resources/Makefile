default:
	migrate -version

migrate create:
	migrate create -ext sql -dir ./migration -seq init

migrate up:
	migrate -path ./migration -database 'postgresql://${POSTGRES_USERNAME}:${POSTGRES_PASSWORD}@localhost/${POSTGRES_DB}' up

migrate down:
	migrate -path ./migration -database 'postgresql://${POSTGRES_USERNAME}:${POSTGRES_PASSWORD}@localhost/${POSTGRES_DB}' down -all