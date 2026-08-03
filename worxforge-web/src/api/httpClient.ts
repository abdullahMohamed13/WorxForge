// TODO: VITE_API_BASE_URL must be set in .env before implementation.
async function request<T>(_path: string, _init?: RequestInit): Promise<T> {
  // TODO: Implementation
  throw new Error("TODO: Implementation");
}

export const httpClient = {
  get: <T>(path: string) => request<T>(path, { method: "GET" }),
  post: <T>(path: string, body: unknown) => request<T>(path, { method: "POST", body: JSON.stringify(body) }),
  put: <T>(path: string, body: unknown) => request<T>(path, { method: "PUT", body: JSON.stringify(body) }),
  delete: <T>(path: string) => request<T>(path, { method: "DELETE" }),
};
