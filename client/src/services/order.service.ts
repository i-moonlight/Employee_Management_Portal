import { Confirmation, Order, TypeOrderData } from '@/models/order.interface';
import { instance } from '@/api/api.interceptor';

const ORDERS = 'orders';

export const OrderService = {

	async getAllOrders() {
		return instance<Order[]>({
			url: ORDERS,
			method: 'GET'
		})
	},

	async getOrderByUserId() {
		return instance<Order[]>({
			url: `${ORDERS}/by-user`,
			method: 'GET'
		})
	},

	async place(data: TypeOrderData) {
		return instance<Confirmation>({
			url: ORDERS,
			method: 'POST',
			data
		})
	}
}
