import { PaymentResponse } from '@/models/payment.interface';
import { instance } from '@/api/api.interceptor';

const PAYMENT = 'payment';

export const PaymentService = {
	async createPayment(amount: number) {
		return instance.post<PaymentResponse>(PAYMENT, { amount });
	}
}
