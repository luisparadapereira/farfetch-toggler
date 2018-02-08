export declare module FarfetchModels {
    export interface TogglerMessage<T> {
        result: T;
        message: string;
    }

    export interface ToggleListDto {
        id: string;
        name: string;
        value: boolean
    }

    export interface ToggleDto {
        id: string;
        name: string;
        value: boolean;
        overrides: boolean;
        serviceList: Array<ServiceDto>
    }

    export interface ServiceDto {
        id: string;
        name: string;
        version: string;
        apiKey: string;
    }
}
