import { combineReducers, configureStore } from '@reduxjs/toolkit';
import { FLUSH, PAUSE, PERSIST, persistReducer, persistStore, PURGE, REGISTER, REHYDRATE } from 'redux-persist';
import { userSlice } from './user/user.slice';

const isClient = typeof window !== 'undefined';

const rootReducer = combineReducers({
	user: userSlice.reducer
});

let persistedReducer = rootReducer;

if (isClient) {
	const { persistReducer } = require('redux-persist')
	const storage = require('redux-persist/lib/storage').default
	const persistConfig = {
		key: 'amazon-shop',
		storage,
		whitelist: ['cart']
	}
	persistedReducer = persistReducer(persistConfig, rootReducer);
}

export const store = configureStore({
	reducer: persistedReducer,
	middleware: getDefaultMiddleware => getDefaultMiddleware({
		serializableCheck: {
			ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER]
		}
	})
});

export const persistor = persistStore(store);

export type TypeRootStore = ReturnType<typeof rootReducer>;
