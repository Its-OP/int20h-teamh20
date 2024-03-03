import { combineReducers } from "@reduxjs/toolkit";
import { userSlice } from "../features/auth/model/authSlice.ts";
import { notificationSlice } from "../enteties/notification/model/notificationSlice.ts";

export const rootReducer = combineReducers({
    user: userSlice.reducer,
    notifications: notificationSlice.reducer
});
