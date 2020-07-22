import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-tile',
  templateUrl: './tile.component.html',
  styleUrls: ['./tile.component.css']
})
export class TileComponent implements OnInit {
  @Input() public label: string = '';
  @Input() public icon: string = '';
  @Input() public background: string = '';


  @Input() title : string = '';
  constructor() { }

  ngOnInit(): void {
  }

}
