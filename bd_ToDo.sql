PGDMP  :    *                |            bd_ToDo    17.2    17.2 *               0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false                       1262    16387    bd_ToDo    DATABASE     |   CREATE DATABASE "bd_ToDo" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Spanish_Spain.1252';
    DROP DATABASE "bd_ToDo";
                     postgres    false            �            1259    16416    USER    TABLE     �   CREATE TABLE public."USER" (
    iduser integer NOT NULL,
    username character varying(256) NOT NULL,
    password character varying(256) NOT NULL,
    email character(256) NOT NULL
);
    DROP TABLE public."USER";
       public         heap r       postgres    false            �            1259    16415    USER_iduser_seq    SEQUENCE     �   CREATE SEQUENCE public."USER_iduser_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public."USER_iduser_seq";
       public               postgres    false    224                       0    0    USER_iduser_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public."USER_iduser_seq" OWNED BY public."USER".iduser;
          public               postgres    false    223            �            1259    16389    assignation    TABLE     �   CREATE TABLE public.assignation (
    idassignation integer NOT NULL,
    idtask integer NOT NULL,
    idlabel integer NOT NULL
);
    DROP TABLE public.assignation;
       public         heap r       postgres    false            �            1259    16388    assignation_idassignation_seq    SEQUENCE     �   CREATE SEQUENCE public.assignation_idassignation_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 4   DROP SEQUENCE public.assignation_idassignation_seq;
       public               postgres    false    218                       0    0    assignation_idassignation_seq    SEQUENCE OWNED BY     _   ALTER SEQUENCE public.assignation_idassignation_seq OWNED BY public.assignation.idassignation;
          public               postgres    false    217            �            1259    16398    categorylabel    TABLE     �   CREATE TABLE public.categorylabel (
    idlabel integer NOT NULL,
    iduser integer,
    name character varying(256) NOT NULL,
    description character(256) NOT NULL
);
 !   DROP TABLE public.categorylabel;
       public         heap r       postgres    false            �            1259    16397    categorylabel_idlabel_seq    SEQUENCE     �   CREATE SEQUENCE public.categorylabel_idlabel_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.categorylabel_idlabel_seq;
       public               postgres    false    220                       0    0    categorylabel_idlabel_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.categorylabel_idlabel_seq OWNED BY public.categorylabel.idlabel;
          public               postgres    false    219            �            1259    16408    task    TABLE     �   CREATE TABLE public.task (
    idtask integer NOT NULL,
    iduser integer,
    description character(256) NOT NULL,
    priority integer NOT NULL,
    startdate timestamp without time zone NOT NULL,
    enddate timestamp without time zone NOT NULL
);
    DROP TABLE public.task;
       public         heap r       postgres    false            �            1259    16407    task_idtask_seq    SEQUENCE     �   CREATE SEQUENCE public.task_idtask_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.task_idtask_seq;
       public               postgres    false    222                        0    0    task_idtask_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.task_idtask_seq OWNED BY public.task.idtask;
          public               postgres    false    221            i           2604    16419    USER iduser    DEFAULT     n   ALTER TABLE ONLY public."USER" ALTER COLUMN iduser SET DEFAULT nextval('public."USER_iduser_seq"'::regclass);
 <   ALTER TABLE public."USER" ALTER COLUMN iduser DROP DEFAULT;
       public               postgres    false    224    223    224            f           2604    16392    assignation idassignation    DEFAULT     �   ALTER TABLE ONLY public.assignation ALTER COLUMN idassignation SET DEFAULT nextval('public.assignation_idassignation_seq'::regclass);
 H   ALTER TABLE public.assignation ALTER COLUMN idassignation DROP DEFAULT;
       public               postgres    false    217    218    218            g           2604    16401    categorylabel idlabel    DEFAULT     ~   ALTER TABLE ONLY public.categorylabel ALTER COLUMN idlabel SET DEFAULT nextval('public.categorylabel_idlabel_seq'::regclass);
 D   ALTER TABLE public.categorylabel ALTER COLUMN idlabel DROP DEFAULT;
       public               postgres    false    219    220    220            h           2604    16411    task idtask    DEFAULT     j   ALTER TABLE ONLY public.task ALTER COLUMN idtask SET DEFAULT nextval('public.task_idtask_seq'::regclass);
 :   ALTER TABLE public.task ALTER COLUMN idtask DROP DEFAULT;
       public               postgres    false    221    222    222                      0    16416    USER 
   TABLE DATA           C   COPY public."USER" (iduser, username, password, email) FROM stdin;
    public               postgres    false    224   �/                 0    16389    assignation 
   TABLE DATA           E   COPY public.assignation (idassignation, idtask, idlabel) FROM stdin;
    public               postgres    false    218   {0                 0    16398    categorylabel 
   TABLE DATA           K   COPY public.categorylabel (idlabel, iduser, name, description) FROM stdin;
    public               postgres    false    220   �0                 0    16408    task 
   TABLE DATA           Y   COPY public.task (idtask, iduser, description, priority, startdate, enddate) FROM stdin;
    public               postgres    false    222   A1       !           0    0    USER_iduser_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public."USER_iduser_seq"', 3, true);
          public               postgres    false    223            "           0    0    assignation_idassignation_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public.assignation_idassignation_seq', 4, true);
          public               postgres    false    217            #           0    0    categorylabel_idlabel_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('public.categorylabel_idlabel_seq', 3, true);
          public               postgres    false    219            $           0    0    task_idtask_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.task_idtask_seq', 4, true);
          public               postgres    false    221            u           2606    16427    USER USER_email_key 
   CONSTRAINT     S   ALTER TABLE ONLY public."USER"
    ADD CONSTRAINT "USER_email_key" UNIQUE (email);
 A   ALTER TABLE ONLY public."USER" DROP CONSTRAINT "USER_email_key";
       public                 postgres    false    224            w           2606    16423    USER USER_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public."USER"
    ADD CONSTRAINT "USER_pkey" PRIMARY KEY (iduser);
 <   ALTER TABLE ONLY public."USER" DROP CONSTRAINT "USER_pkey";
       public                 postgres    false    224            y           2606    16425    USER USER_username_key 
   CONSTRAINT     Y   ALTER TABLE ONLY public."USER"
    ADD CONSTRAINT "USER_username_key" UNIQUE (username);
 D   ALTER TABLE ONLY public."USER" DROP CONSTRAINT "USER_username_key";
       public                 postgres    false    224            k           2606    16394    assignation assignation_pkey 
   CONSTRAINT     e   ALTER TABLE ONLY public.assignation
    ADD CONSTRAINT assignation_pkey PRIMARY KEY (idassignation);
 F   ALTER TABLE ONLY public.assignation DROP CONSTRAINT assignation_pkey;
       public                 postgres    false    218            o           2606    16405     categorylabel categorylabel_pkey 
   CONSTRAINT     c   ALTER TABLE ONLY public.categorylabel
    ADD CONSTRAINT categorylabel_pkey PRIMARY KEY (idlabel);
 J   ALTER TABLE ONLY public.categorylabel DROP CONSTRAINT categorylabel_pkey;
       public                 postgres    false    220            r           2606    16413    task task_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.task
    ADD CONSTRAINT task_pkey PRIMARY KEY (idtask);
 8   ALTER TABLE ONLY public.task DROP CONSTRAINT task_pkey;
       public                 postgres    false    222            p           1259    16406    crea_fk    INDEX     C   CREATE INDEX crea_fk ON public.categorylabel USING btree (iduser);
    DROP INDEX public.crea_fk;
       public                 postgres    false    220            l           1259    16395    pertenece2_fk    INDEX     H   CREATE INDEX pertenece2_fk ON public.assignation USING btree (idlabel);
 !   DROP INDEX public.pertenece2_fk;
       public                 postgres    false    218            m           1259    16396    pertenece_fk    INDEX     F   CREATE INDEX pertenece_fk ON public.assignation USING btree (idtask);
     DROP INDEX public.pertenece_fk;
       public                 postgres    false    218            s           1259    16414    tiene_fk    INDEX     ;   CREATE INDEX tiene_fk ON public.task USING btree (iduser);
    DROP INDEX public.tiene_fk;
       public                 postgres    false    222            z           2606    16433 *   assignation fk_assignat_pertenece_category    FK CONSTRAINT     �   ALTER TABLE ONLY public.assignation
    ADD CONSTRAINT fk_assignat_pertenece_category FOREIGN KEY (idlabel) REFERENCES public.categorylabel(idlabel);
 T   ALTER TABLE ONLY public.assignation DROP CONSTRAINT fk_assignat_pertenece_category;
       public               postgres    false    218    4719    220            {           2606    16428 &   assignation fk_assignat_pertenece_task    FK CONSTRAINT     �   ALTER TABLE ONLY public.assignation
    ADD CONSTRAINT fk_assignat_pertenece_task FOREIGN KEY (idtask) REFERENCES public.task(idtask);
 P   ALTER TABLE ONLY public.assignation DROP CONSTRAINT fk_assignat_pertenece_task;
       public               postgres    false    4722    218    222            |           2606    16438 #   categorylabel fk_category_crea_user    FK CONSTRAINT     �   ALTER TABLE ONLY public.categorylabel
    ADD CONSTRAINT fk_category_crea_user FOREIGN KEY (iduser) REFERENCES public."USER"(iduser);
 M   ALTER TABLE ONLY public.categorylabel DROP CONSTRAINT fk_category_crea_user;
       public               postgres    false    4727    220    224            }           2606    16443    task fk_task_tiene_user    FK CONSTRAINT     z   ALTER TABLE ONLY public.task
    ADD CONSTRAINT fk_task_tiene_user FOREIGN KEY (iduser) REFERENCES public."USER"(iduser);
 A   ALTER TABLE ONLY public.task DROP CONSTRAINT fk_task_tiene_user;
       public               postgres    false    4727    224    222               r   x��л�0E�X��
4�m�BH4����k���$4�������U��z>�_�{ru7�x����� ���Z@����7Q��W��x�<���Abތ�7Q[�1�:�osφ�             x�3�4�4�2�B.cN �2�4�1z\\\ 4�v         �   x�ݐ1
AE��sawO`��XX�|3)F�ɒo� "�"�>��o��N���~��i+-�SD�\3$YADb%hm9_+<�HG��gژ���GW#��<Y��Ͽ�'�耗�i}�uA�����3?I��9         �   x��ұ
�0�9y�{���5Ztqqt	$PI���3�b�XC�{:�s�?�}	'�B��`^O��d�Ȫ"�� ��4�K2�|:ɂŮ��<l��&D뺹(�D��8�̈́��x�E-��v����\u����O"�4K 5�b��Ԉ�C4��˗����_���r�R�7v��     