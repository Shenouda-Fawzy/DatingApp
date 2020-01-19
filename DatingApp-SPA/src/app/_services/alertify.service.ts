import { Injectable } from "@angular/core";
import * as alertify from "alertifyjs";
@Injectable({
  providedIn: "root"
})
export class AlertifyService {
  constructor() {}

  confirm(message: string, callBackFunction: () => any) {
    alertify.confirm(message, (e: any) => {
      if (e) {
        callBackFunction();
      } else {
        // in case the user press cancel, So do nothing.
      }
    });
  }

  success(message: string){
    alertify.success(message);
  }

  error(message: string){
    alertify.error(message);
  }

  warining(message: string){
    alertify.warning(message);
  }


  message(message: string){
    alertify.message(message);
  }


  
}
