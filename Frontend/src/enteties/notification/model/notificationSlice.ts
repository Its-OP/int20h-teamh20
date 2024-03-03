import { createSlice, PayloadAction } from "@reduxjs/toolkit";

export type NotificationState = {
    amount: number;
    show: boolean;
};

const initialState: NotificationState = {
    amount: 0,
    show: false
};

export const notificationSlice = createSlice({
    name: "notification",
    initialState,
    reducers: {
        setNotifcationAmount: (
            state,
            action: PayloadAction<NotificationState>
        ) => {
            state.amount = action.payload.amount;
        },
        resetNotification: state => {
            state.amount = 0;
        },
        showNotification: state => {
            state.show = true;
        },
        hideNotification: state => {
            state.show = false;
        }
    }
});
