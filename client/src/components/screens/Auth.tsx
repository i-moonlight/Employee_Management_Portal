import { FC, useState } from 'react';
import { EmailPassword } from '@/store/user/user.interface';
import Field from '@/ui/input/Field';
import Heading from '@/ui/Heading';
import Loader from '@/ui/Loader';
import Meta from '@/ui/Meta';
import { useAuth } from '@/hooks/useAuth';
import { useActions } from '@/hooks/useActions';
import { useForm, SubmitHandler } from 'react-hook-form';
import { useAuthRedirect } from '@/hooks/useAuthRedirect';
import { validEmail } from '@/ui/validate/valid-email';

const Auth: FC = () => {

	useAuthRedirect();

	const { isLoading } = useAuth();
	const { login, register } = useActions();
	const [type, setType] = useState<'login' | 'register'>('login');
	const {
		register: formRegister,
		handleSubmit,
		formState: { errors },
		reset
	} = useForm<EmailPassword>({ mode: 'onChange' });

	const onSubmit: SubmitHandler<EmailPassword> = data => {
		if (type === 'login') login(data);
		else register(data);
		reset();
	}

	return (
		<Meta title='Auth'>
			<section className='flex h-screen'>
				<form
					onSubmit={handleSubmit(onSubmit)}
					className='rounded-sm bg-white shadow-sm p-8 m-auto'
				>
					<Heading className='capitalize text-center mb-4'>{type}</Heading>
					{isLoading && <Loader />}
					<Field
						{...formRegister('email', {
							required: 'Email is required',
							pattern: {
								value: validEmail,
								message: 'Please enter valid email address'
							}
						})}
						placeholder='Email'
						error={errors.email?.message}
					/>

					<Field
						{...formRegister('password', {
							required: 'Password is required',
							minLength: {
								value: 6,
								message: 'min length should more 6 symbols'
							}
						})}
						placeholder='Password'
						error={errors.password?.message}
					/>
					{/*<Button type='submit' variant='orange'>*/}
					{/*	let's go!*/}
					{/*</Button>*/}

					<div>
						<button
							type='button'
							className='inline-block opacity-40 mt-3 text-sm'
							onClick={() => {
								setType(type === 'login' ? 'register' : 'login');
							}}
						>
							{type === 'login' ? 'Register' : 'Login'}
						</button>
					</div>
				</form>
			</section>
		</Meta>
	);
}

export default Auth
