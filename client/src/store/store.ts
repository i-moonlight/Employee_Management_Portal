import { combineReducers, configureStore } from '@reduxjs/toolkit';
import { FLUSH, PAUSE, PERSIST, persistReducer, persistStore, PURGE, REGISTER, REHYDRATE } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import { cartSlice } from '@/store/cart/cart.slice';
import { userSlice } from '@/store/user/user.slice';

//const isClient = typeof window !== 'undefined';
// let persistedReducer = rootReducer;
//
// if (isClient) {
// 	const { persistReducer } = require('redux-persist')
// 	const storage = require('redux-persist/lib/storage').default
// 	const persistConfig = {
// 		key: 'amazon-shop',
// 		storage,
// 		whitelist: ['cart']
// 	}
// 	persistedReducer = persistReducer(persistConfig, rootReducer);
// }

// export const store = configureStore({
// 	reducer: persistedReducer,
// 	middleware: getDefaultMiddleware => getDefaultMiddleware({
// 		serializableCheck: {
// 			ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER]
// 		}
// 	})
// });

const rootReducer = combineReducers({
	cart: cartSlice.reducer,
	user: userSlice.reducer
});

const persistConfig = {
	key: 'root',
	version: 1,
	storage,
};

const persistedReducer = persistReducer(persistConfig, rootReducer);

export const store = configureStore({
	reducer: { next: persistedReducer },
	middleware: (getDefaultMiddleware) => getDefaultMiddleware({
		serializableCheck: {
			ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
		}
	})
});

export const persistor = persistStore(store);

// export type TypeRootState = ReturnType<typeof rootReducer>;
