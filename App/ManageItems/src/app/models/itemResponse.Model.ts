import { ItemImage } from './item-image.model';

export interface ItemResponse {
  id: string;
  userId: string;
  code: string;
  brand: string;
  name: string;
  sellingPrice: number;
  description: string;
  isApproved: boolean;
  createdDate: Date;
  itemImages: ItemImage[] | null;
}

export interface ItemsRespose {
  items: ItemResponse[];
  totalRecord: number;
}
