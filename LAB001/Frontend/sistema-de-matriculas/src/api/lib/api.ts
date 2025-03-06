import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse, InternalAxiosRequestConfig } from 'axios';

class ApiService {
    private api: AxiosInstance;

    constructor(baseURL: string) {
        this.api = axios.create({
            baseURL,
        });

        this.api.interceptors.request.use(this.handleRequest);
        this.api.interceptors.response.use(this.handleResponse, this.handleError);
    }

    private handleRequest(config: InternalAxiosRequestConfig): InternalAxiosRequestConfig {
        return config;
    }

    private handleResponse(response: AxiosResponse): AxiosResponse {
        return response;
    }

    private handleError(error: unknown): Promise<unknown> {
        return Promise.reject(error);
    }

    public get<T>(url: string, config?: AxiosRequestConfig): Promise<AxiosResponse<T>> {
        return this.api.get<T>(url, config);
    }

    public post<T>(url: string, data?: T, config?: AxiosRequestConfig): Promise<AxiosResponse<T>> {
        return this.api.post<T>(url, data, config);
    }

    public put<T>(url: string, data?: T, config?: AxiosRequestConfig): Promise<AxiosResponse<T>> {
        return this.api.put<T>(url, data, config);
    }

    public delete<T>(url: string, config?: AxiosRequestConfig): Promise<AxiosResponse<T>> {
        return this.api.delete<T>(url, config);
    }
}

const api = new ApiService('http://localhost:5148');

export default api;
