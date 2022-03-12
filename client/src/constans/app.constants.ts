export const SITE_NAME = "Ecommerce";
export const SITE_DESCRIPTION = "Ecommerce portal";
export const NO_INDEX_PAGE = { robots: { index: false, follow: false } }

export const ADMIN_PANEL_URL = '/admin';
export const getSiteUrl = () => process.env.NEXT_PUBLIC_APP_URL as string;
export const getAdminUrl = (path = '') => `${ADMIN_PANEL_URL}${path}`;
