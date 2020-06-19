import { Component, OnInit, TemplateRef } from '@angular/core';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css'],
  host: {'[class.ngb-toasts]': 'true'}
})
export class ToastComponent implements OnInit {

  constructor(public toastService: ToastService) { }

  ngOnInit() {
  }

  isTemplate(toast) { return toast.textOrTpl instanceof TemplateRef; }
}
