import { combineReducers, configureStore } from '@reduxjs/toolkit';
import { FLUSH, PAUSE, PERSIST, PURGE, REGISTER, REHYDRATE } from 'redux-persist';
import { userSlice } from './user/user.slice';

const isClient = typeof window !== 'undefined';

const combinedReducers = combineReducers({
	user: userSlice.reducer
});

let mainReducer = combinedReducers;

if (isClient) {
	const { persistReducer } = require('redux-persist')
	const storage = require('redux-persist/lib/storage').default
	const persistConfig = {
		key: 'amazon-shop',
		storage,
		whitelist: ['cart']
	}
	mainReducer = persistReducer(persistConfig, combinedReducers);
}

export const store = configureStore({
	reducer: mainReducer,
	middleware: getDefaultMiddleware => getDefaultMiddleware({
		serializableCheck: {
			ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER]
		}
	});
});
