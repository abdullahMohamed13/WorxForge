export type NotificationType =
  | "AssetUpdated"
  | "CommentOnAsset"
  | "CommentReply"
  | "AssetPublished"
  | "Announcement"
  | "FollowNotification";

export interface Notification {
  notificationId: string;
  type: NotificationType;
  message: string;
  assetId?: string;
  commentId?: string;
  isRead: boolean;
  createdAt: string;
}

export interface UnreadNotificationCount {
  count: number;
}
