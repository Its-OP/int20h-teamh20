import { createSlice, PayloadAction } from "@reduxjs/toolkit";
export type UserId = null | number;
export enum userRole {
    Professor = "Professor",
    Student = "Student"
}

export type UserState = {
    userId: UserId;
    role: userRole | null;
};

const initialState: UserState = {
    userId: null,
    role: null
};

export const userSlice = createSlice({
    name: "user",
    initialState,
    reducers: {
        login: (state, action: PayloadAction<UserState>) => {
            state.userId = action.payload.userId;
            state.role = action.payload.role;
        },
        logout: state => {
            state.userId = null;
            state.role = null;
        }
    }
});
