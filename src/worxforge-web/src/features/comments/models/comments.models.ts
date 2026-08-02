export interface CreateCommentRequest {
  assetId: string;
  parentCommentId?: string;
  content: string;
}
