import { createSlice, PayloadAction } from "@reduxjs/toolkit";
export type UserId = null | number;
export type UserName = null | number;
export type UserState = {
    userId: UserId;
    userName: UserName;
};

const initialState: UserState = {
    userId: null,
    userName: null
};

export const userSlice = createSlice({
    name: "user",
    initialState,
    reducers: {
        login: (state, action: PayloadAction<UserState>) => {
            state.userId = action.payload.userId;
            state.userName = action.payload.userName;
        },
        logout: state => {
            state.userId = null;
        }
    }
});
