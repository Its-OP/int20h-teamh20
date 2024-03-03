import { useState } from "react";
import { Notification } from "../../../shared/model/types.ts";
import { HTTP_METHOD, useHttp } from "../../../shared/api/useHttp.ts";
import { urls } from "../../../shared/api/api.ts";
import { message } from "antd";
import { errNotificationMessage } from "../../../shared/model/const.ts";

export const useNotifications = () => {
    const { messages } = urls;
    const { request, loading } = useHttp();
    const [notifications, setNotifications] = useState<Notification[]>([]);

    const fetchNotifications = async () => {
        const res = await request(messages);

        if (res) {
            setNotifications(res);
        } else {
            message.error(errNotificationMessage);
        }
    };

    const readNotification = async (id: number) => {
        const res = await request(`${messages}read/${id}`, HTTP_METHOD.POST);
        return res?.success;
    };

    return {
        fetchNotifications,
        notifications,
        notificationLoading: loading,
        readNotification
    };
};
