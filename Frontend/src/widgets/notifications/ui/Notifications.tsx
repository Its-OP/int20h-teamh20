import { FC, useState } from "react";
import { Col, Collapse, Modal, Row, Typography } from "antd";
import { useAppDispatch, useAppSelector } from "../../../App/appStore.ts";
import { notificationSlice } from "../../../enteties/notification/model/notificationSlice.ts";
import { useNotifications } from "../../../enteties/notification/api/useNotifications.ts";
import { Notification } from "../../../shared/model/types.ts";

const { Panel } = Collapse;

const Noty: FC<{
    noti: Notification;
    readNotification: (id: number) => Promise<boolean>;
}> = ({ noti, readNotification }) => {
    const [noty, setNoty] = useState<Notification>(noti);
    const read = async () => {
        const res = await readNotification(noti.id);
        if (res) {
            setNoty(prev => ({ ...prev, isRead: true }));
        }
    };

    const notyHandler = noti.isRead ? async () => {} : read;

    return (
        <Collapse onChange={notyHandler}>
            <Panel key={noty.id} header={noty.title}>
                <Typography>{noty.text}</Typography>
            </Panel>
        </Collapse>
    );
};

const Notifications: FC = () => {
    const dispatch = useAppDispatch();
    const actions = notificationSlice.actions;
    const {
        notifications,
        // fetchNotifications,
        // notificationLoading,
        readNotification
    } = useNotifications();

    const { show } = useAppSelector(state => state.notifications);

    const hideModal = () => dispatch(actions.hideNotification());

    return (
        <Modal
            open={show}
            onCancel={hideModal}
            title={"Повідомленя"}
            footer={null}
        >
            <Row>
                <Col xs={24}>
                    {notifications.map(noti => {
                        return (
                            <Noty
                                noti={noti}
                                readNotification={readNotification}
                            />
                        );
                    })}
                </Col>
            </Row>
        </Modal>
    );
};

export default Notifications;
