import { ErrorHandler } from '@angular/core';

export class ExceptionHandler implements ErrorHandler {
    handleError(error: any): void {
        console.error('--- CAUGHT ERROR ---');
        console.log(error);
        console.log('----');
    }
}
