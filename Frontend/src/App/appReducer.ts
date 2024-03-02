import { combineReducers } from "@reduxjs/toolkit";
import { userSlice } from "../features/auth/model/authSlice.ts";

export const rootReducer = combineReducers({
    user: userSlice.reducer
});
