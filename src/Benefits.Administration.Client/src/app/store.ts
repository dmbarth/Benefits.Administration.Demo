import { configureStore, getDefaultMiddleware } from '@reduxjs/toolkit';

import rootReducer from './reducer';

export type RootState = ReturnType<typeof rootReducer>

export default configureStore({
    reducer: rootReducer,
    preloadedState: {},
    middleware:  getDefaultMiddleware()
});;